DELIMITER $$
CREATE PROCEDURE Sp_InsertUpdateStudentsWithSubjects(
    IN MetaData TEXT,
    IN Username VARCHAR(40)
)
BEGIN

 DECLARE LengthItems INT DEFAULT JSON_LENGTH(MetaData);
    DECLARE idx INT DEFAULT 0;
    DECLARE HasError BOOL DEFAULT FALSE;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET HasError = TRUE;
        ROLLBACK;
        RESIGNAL;
    END;

    START TRANSACTION;

    SET @SqlInsert = '';

    WHILE idx < LengthItems DO

        SET @Idcontrol = JSON_UNQUOTE(JSON_EXTRACT(MetaData, CONCAT('$[', idx, '].Idcontrol')));
        SET @IdFkStudent = JSON_UNQUOTE(JSON_EXTRACT(MetaData, CONCAT('$[', idx, '].IdFkStudent')));
        SET @IdFkSubject = JSON_UNQUOTE(JSON_EXTRACT(MetaData, CONCAT('$[', idx, '].IdFkSubject')));
        SET @IdFkSubjectAssignation = JSON_UNQUOTE(JSON_EXTRACT(MetaData, CONCAT('$[', idx, '].IdFkSubjectAssignation')));

        IF @Idcontrol IS NULL OR @Idcontrol = 'null' THEN
            -- Armar parte del insert
            IF LENGTH(@SqlInsert) > 0 THEN
                SET @SqlInsert = CONCAT(@SqlInsert, ' 
                UNION ALL 
                SELECT "', @IdFkStudent, '", "', @IdFkSubject, '", "', @IdFkSubjectAssignation, '", "', Username, '"');
            ELSE
                SET @SqlInsert = CONCAT('
                SELECT "', @IdFkStudent, '", "', @IdFkSubject, '", "', @IdFkSubjectAssignation, '", "', Username, '"');
            END IF;
        ELSE
            -- Ejecutar UPDATE directamente para evitar error de múltiples statements
            SET @sql = CONCAT(
                'UPDATE studentwithsubjects s ',
                'SET s.IdFkStudent = ', @IdFkStudent, 
                ', s.IdFkSubject = ', @IdFkSubject, 
                ', s.IdFkSubjectAssignation = ', @IdFkSubjectAssignation, 
                ' WHERE s.Idcontrol = ', @Idcontrol
            );

            PREPARE stmt FROM @sql;
            EXECUTE stmt;
            DEALLOCATE PREPARE stmt;
        END IF;

        SET idx = idx + 1;
    END WHILE;

    -- Ejecutar el insert si hay datos pendientes
    IF LENGTH(@SqlInsert) > 0 THEN
        SET @SqlInsert = CONCAT(
            'INSERT INTO studentwithsubjects (IdFkStudent, IdFkSubject, IdFkSubjectAssignation, Username) ',
            @SqlInsert
        );
        PREPARE stmt FROM @SqlInsert;
        EXECUTE stmt;
        DEALLOCATE PREPARE stmt;
    END IF;

    IF NOT HasError THEN
        COMMIT;
        SELECT 'Transacción completada exitosamente' AS Resultado;
    END IF;

END$$

DELIMITER $$
CREATE PROCEDURE Sp_TeacherAll()
BEGIN
SELECT d.* FROM SubjectsWithTeacherWithDescription d;
	
END$$



#Token 
#qYbRKmGXAk8lmd74usTeE7M2HBb8sEzjO778RkuWh4rrA5DhTFQaWVrtEG+dtksn1SFkdlVUQf+Z5l6Vr7rQTyezJWTi3ufVUB5ELIdjImI=