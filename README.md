# ProjectManagerSystem

Requirements:

• Projects are used to organize tasks into logical units. Required attributes are: project name and project code
(unique).

• Tasks are used as a child of Project. One Task can belong to one Project, and one project can have many
tasks. Required attributes for tasks are: assignee (user to whom this tickets is assigned to), status (new, in
progress, finished), progress (% of completed), deadline, description.

• Users – required attributes: username (unique), email, name and surname and role.

• Roles – required roles: Administrator, Project Manager or Developer

• A system for tracking Tasks and User assignments is administered by the Administrator, who is allowed to do
any of the scenarios defined below.
Scenarios:

● All Users use the same login form.

● Administrator can view, create, modify and delete Projects, Tasks and Users.
○ If Administrator is creating Project, he must select Project Manager to manage the project.
Project managers are Users with Role Project Manager.

● Administrator can assign / unassign a Task to a User.

● Project Managers can create Projects, Tasks and assign Tasks to the Developers.

● Developers cannot have more than 3 tasks assigned

● User can modify Task only if it is assigned to him / her.
○ User with Developer role is allowed to change: status, progress, description
○ User with Project Manager role is allowed to change: assignee, status, progress, deadline,
description.

● User can view list of tasks that are assigned to him / her.
○ If a task deadline is less than 5 days away it should be marked somehow on task
overview.

● Project manager can view a list of projects and their details like:
○ Project progress is based on progress of all project tasks. (e.g. project with three tasks
with progress 0%, 50% and 100% has progress 50%)
○ Task summary - total number of tasks and number of tasks in each status
○ Number of tasks that are overdue, or are going to be in 2 days (Deadline < Current time
+ 2 days)
