
use studentregdb;

CREATE VIEW UsersAllForLoginViews AS 
 SELECT 
 u.Identification,
 u.Password,
 u.Rol,
 u.Enabled
 FROM studentregdb.users u
 WHERE u.Enabled = TRUE;
 
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
 RIGHT JOIN subjectsassignation sj ON sj.Idcontrol = sb.IdFkSubject AND sj.Enabled = TRUE
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
 RIGHT JOIN subjectsassignation sj ON sj.Idcontrol = sb.IdFkSubject AND sj.Enabled = TRUE
 WHERE sb.Enabled = TRUE;
 