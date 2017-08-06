SELECT *
FROM
	TimeLog TL INNER JOIN TaskLog TK ON (TL.TaskLogID = TK.TaskLogID)
	INNER JOIN Tasks T ON (TK.TaskID = T.TaskID)
ORDER BY TimeLogID DESC

UpDATE TimeLog SET ActionTime = '2010-03-30 18:38:46.000' WHERE TimeLogID = 1163

INSERT INTO Tasks (Task)
VALUES('Lutron - Workflow Review')

INSERT INTO TaskLog(TaskDate, UserID, TaskID, TaskStatusID)
VALUES(CONVERT(datetime, CONVERT(varchar(10), GETDATE(), 12)), 'cgray', scope_identity(), 1)

UPDATE
TimeLog
SET
TaskLogID = scope_identity()
WHERE
	TimeLogID IN (1010, 1011)