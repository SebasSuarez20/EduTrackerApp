
use studentregdb;

CREATE VIEW UsersAllForLoginViews AS 
    SELECT 
        `u`.`Idcontrol` AS `Idcontrol`,
        `u`.`Identification` AS `Identification`,
        `u`.`Password` AS `Password`,
        `u`.`Rol` AS `Rol`,
        `u`.`Enabled` AS `Enabled`
    FROM
        `studentregdb`.`users` `u`
    WHERE
        (`u`.`Enabled` = TRUE);
 
 CREATE VIEW VisualizationStudentsWithCompanions
 AS 
 SELECT 
 sb.Idcontrol,
s.FirstName,
s.Identification AS Username,
sj.Name,
sb.IdFkSubject,
sj.Day,
sj.HoursInitial,
sj.HoursFinal
 FROM studentwithsubjects sb
 INNER JOIN students s ON s.Idcontrol = sb.IdFkStudent AND s.Enabled = TRUE
 RIGHT JOIN subjects sj ON sj.Idcontrol = sb.IdFkSubject AND sj.Enabled = TRUE
 WHERE sb.Enabled = TRUE;
 
  CREATE VIEW VisualizationTeacher
 AS 
 SELECT 
 sb.Idcontrol,
s.FirstName,
s.Identification AS Username,
sj.Name,
sb.IdFkSubject,
sj.Day,
sj.HoursInitial,
sj.HoursFinal
 FROM studentwithsubjects sb
 INNER JOIN students s ON s.Idcontrol = sb.IdFkStudent AND s.Enabled = TRUE
 RIGHT JOIN subjects sj ON sj.Idcontrol = sb.IdFkSubject AND sj.Enabled = TRUE
 WHERE sb.Enabled = TRUE;
 
 
 CREATE VIEW SubjectsWithTeacherWithDescription AS
    SELECT 
    sa.Idcontrol AS IdxSubjectAssignation,
    sa.FkIdControlSubject AS IdxSubject,
    sa.Name,sa.Day,sa.HoursInitial,sa.HoursFinal,
    CONCAT_WS(' ',t.FirstName,t.FirstSurname) AS NameTeacher,
     ROW_NUMBER() OVER(PARTITION BY sa.FkIdControlSubject) AS Idx
    FROM teachers t
    INNER JOIN subjectswithteacher ts ON ts.IdFkTeacher = t.Idcontrol AND ts.Enabled = TRUE
    INNER JOIN subjectsassignation sa ON sa.FkIdControlSubject = ts.Idcontrol AND sa.Enabled = TRUE
    WHERE t.Enabled = TRUE;
    
   CREATE VIEW consultinformationstudentview 
   AS
    SELECT
    s.Idcontrol AS IdxSubjectStudent,
    sa.Idcontrol AS IdxSubjects,
    sa.Name AS Name,
    sja.Idcontrol AS IdxAssignation,
    s.IdFkStudent AS IdFkStudent,
    sja.HoursInitial AS HoursInitial,
    sja.HoursFinal AS HoursFinal,
    sja.Day AS Day,
    ROW_NUMBER() OVER (PARTITION BY sa.Name) AS Idx,
    CONCAT_WS(' ', sd.FirstName, sd.FirstSurname) AS NameStudent,

    s.Enabled AS Enabled

FROM studentwithsubjects s
INNER JOIN subjects sa ON 
    sa.Idcontrol = s.IdFkSubject AND
    sa.Enabled = TRUE
INNER JOIN subjectsassignation sja ON 
    sja.FkIdControlSubject = s.IdFkSubject AND
    s.IdFkSubjectAssignation = sja.Idcontrol AND
    sja.Enabled = TRUE
LEFT JOIN students sd ON 
    sd.Idcontrol = s.IdFkStudent AND
    sd.Enabled = TRUE
WHERE s.Enabled = TRUE;
    
