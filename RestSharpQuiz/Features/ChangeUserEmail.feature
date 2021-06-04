Feature: Change user email

Background: 
	Given User signs up in system
	And User signs in
	
Scenario: CHANGE_USER_EMAIL_1_[/quiz/users/email]_[PUT]_send_request_with_correct_email_and_password
	Given User filled correctly data
	When Request sends to API 
	Then The server should return status 200
	And Response with success message

Scenario: CHANGE_USER_EMAIL_2_[/quiz/users/email]_[PUT]_send_request_with_email_which_is_assigned_to_account
	Given User filled email which is assigned to account
	When Request sends to API 
	Then The server should return status 400
	And Response with message about already assigned email

Scenario: CHANGE_USER_EMAIL_3_[/quiz/users/email]_[PUT]_send_request_with_password_which_is_not_assigned_to_account
	Given User filled password which is incorrect
	When Request sends to API 
	Then The server should return status 400
	And Response with message about password

Scenario Outline: CHANGE_USER_EMAIL_4_[/quiz/users/email]_[PUT]_send_request_with_incorrect_email
	Given User filled email '<type of mistake>' with '<data>'
	When Request sends to API 
	Then The server should return status 400
	And Response with message about incorrect email

Examples: 
| type of mistake      | data                                                                                                                                                                                                                                                                    |
| without @            | exmapleEmail.com                                                                                                                                                                                                                                                        |
| which is too long    | xv5XfZ1LXURRkaFvIEvzp7j8Fuj16dziBW9Pv8quGJsdQfOnyKV6hosAlndp2Au244iHlJeHIaQHx2rqzcpyiwjqDywrzFz6CgCvUVVVngr2IkTfDQBsB88llpJYJWY2xbOdvLIBXQ2QOM65PlCBp0TTVQX0lBvFLIAZg7kZNM2hQIN3bpvQ2GaacERotQuF3JPwlvUUr84B9h81Y4z0MmP1hrz1bDaoAzlU5jJx3ft9dCJLXUMUgig4rDDOv@email.com |
| without data after @ | exmapleEmail@                                                                                                                                                                                                                                                           |
| without data         | empty                                                                                                                                                                                                                                                                   |

Scenario Outline: CHANGE_USER_EMAIL_5_[/quiz/users/email]_[PUT]_send_request_with_incorrect_password
	Given User filled password '<type of mistake>' with '<data>'
	When Request sends to API 
	Then The server should return status 400
	And Response with message about incorrect password

Examples: 
| type of mistake       | data                                    |
| which is too short    | short                                   |
| which is too long     | userEnteredTooLongPassword@WhichIsWrong |
| without data          | empty                                   |
| without special key   | password                                |
| which is not a string | float                                   |
