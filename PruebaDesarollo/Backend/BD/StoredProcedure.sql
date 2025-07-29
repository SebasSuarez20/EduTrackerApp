DELIMITER $$
CREATE PROCEDURE Sp_InsertUpdateStudentsWithSubjects(
    IN MetaData TEXT,
    IN Username VARCHAR(40)
)
BEGIN

	DECLARE LengthItems INT DEFAULT JSON_LENGTH(MetaData);
    DECLARE idx INT DEFAULT 0;
    DECLARE HandlerFinish TEXT;
	DECLARE HasError BOOL DEFAULT FALSE;


DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
     SET HasError = TRUE;
        ROLLBACK;
        RESIGNAL;
    END;

    START TRANSACTION;

    SET @SqlInsert = '';
    SET @SqlUpdate = '';

    WHILE idx < LengthItems DO

        SET @Idcontrol = JSON_UNQUOTE(JSON_EXTRACT(MetaData, CONCAT('$[', idx, '].Idcontrol')));
		SET @IdFkStudent = JSON_UNQUOTE(JSON_EXTRACT(MetaData, CONCAT('$[', idx, '].IdFkStudent')));
		SET @IdFkSubject = JSON_UNQUOTE(JSON_EXTRACT(MetaData, CONCAT('$[', idx, '].IdFkSubject')));

        
        IF @Idcontrol IS NULL OR @Idcontrol = 'null' THEN
        
            IF LENGTH(@SqlInsert) > 0 THEN
                SET @SqlInsert = CONCAT(@SqlInsert, ' 
                UNION ALL 
                SELECT "', @IdFkStudent, '", "', @IdFkSubject, '",','"',Username,'"');
            ELSE
                SET @SqlInsert = CONCAT('
               SELECT "', @IdFkStudent, '", "', @IdFkSubject, '",','"',Username,'"');
            END IF;
            
            ELSE 
              SET @SqlUpdate = CONCAT(@SqlUpdate," UPDATE studentwithsubjects s SET s.IdFkStudent = ",@IdFkStudent,", s.IdFkSubject = ",@IdFkSubject," WHERE s.Idcontrol = ",@Idcontrol,";","\n");
        END IF;

        SET idx = idx + 1;
    END WHILE;

 IF LENGTH(@SqlUpdate) > 0 THEN
    PREPARE stmt FROM @SqlUpdate;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;
END IF;

IF LENGTH(@SqlInsert) > 0 THEN
   SET @SqlInsert = CONCAT('INSERT INTO studentwithsubjects (IdFkStudent, IdFkSubject, Username) ',@SqlInsert);
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

	SELECT 
     s.*,
     ROW_NUMBER() OVER(PARTITION BY s.FkIdControlSubject) AS idx
    FROM subjectsassignation s 
    WHERE s.Enabled = TRUE;
	
END$$

#Token 
#qYbRKmGXAk8lmd74usTeE7M2HBb8sEzjO778RkuWh4rrA5DhTFQaWVrtEG+dtksn1SFkdlVUQf+Z5l6Vr7rQTyezJWTi3ufVUB5ELIdjImI=
#DcVTDmqrPfthUbSLzTddcUpKX28AdfDvq/qURKilOP3jczYv7y6hS6XHkqQ7bdS61dDbPt0uheZ7x37504ZM2g==