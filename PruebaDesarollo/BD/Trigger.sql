DELIMITER $$

CREATE TRIGGER studentwithsubjects_BEFORE_INSERT
BEFORE INSERT ON studentwithsubjects
FOR EACH ROW
BEGIN
     DECLARE countData INT DEFAULT 0;
    DECLARE countValidator INT DEFAULT 0;
    
 SELECT IFNULL(SUM(SUB.auxSum) ,0) INTO countValidator FROM (
    SELECT (COUNT(*)) AS auxSum FROM studentwithsubjects sw WHERE
    sw.IdFkStudent = 2 AND sw.Enabled = TRUE
    GROUP BY sw.IdFkStudent,sw.IdFkSubject,sw.Enabled
 ) SUB;

    IF countValidator < 9 THEN 
    
      SELECT COUNT(*) INTO countData
    FROM studentwithsubjects
    WHERE IdFkStudent = NEW.IdFkStudent
      AND IdFkSubject = NEW.IdFkSubject
      AND IdFkSubjectAssignation = NEW.IdFkSubjectAssignation
      AND Enabled = TRUE;

    IF countData > 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: ya existe el estudiante enlazado a ese profesor.';
    END IF;
    
    ELSE 
    
         SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: ya existe tres materias registrada por el estudiante.';
    
    END IF;
END$$

DELIMITER ;
