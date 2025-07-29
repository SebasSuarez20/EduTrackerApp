DELIMITER $$

CREATE TRIGGER studentwithsubjects_BEFORE_INSERT
AFTER INSERT ON studentwithsubjects
FOR EACH ROW
BEGIN
    DECLARE countData INT;

    SELECT COUNT(*) INTO countData
    FROM studentwithsubjects
    WHERE IdFkStudent = NEW.IdFkStudent
      AND IdFkSubject = NEW.IdFkSubject
      AND Enabled = TRUE;

    IF countData > 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: ya existe el estudiante enlazado a ese profesor.';
    END IF;
END$$

DELIMITER ;
