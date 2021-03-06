Feature: Change user email

Background: 
	Given User registers into system and logs in
	
Scenario: CHANGE_USER_EMAIL_[/quiz/users/email]_[PATCH]_1_send_request_with_correct_email_and_password
	Given User filled correctly data
	When Request sends to API 
	Then The server should return status 200 on success
	And Response with a new token

Scenario: CHANGE_USER_EMAIL_[/quiz/users/email]_[PATCH]_2_send_request_with_email_which_is_assigned_to_account
	Given User filled email which is assigned to account
	When Request sends to API 
	Then The server should return status 400
	And Response with message about already assigned email

Scenario: CHANGE_USER_EMAIL_[/quiz/users/email]_[PATCH]_3_send_request_with_password_which_is_not_assigned_to_account
	Given User filled password which is incorrect
	When Request sends to API 
	Then The server should return status 400
	And Response with message about password

Scenario Outline: CHANGE_USER_EMAIL_[/quiz/users/email]_[PATCH]_4_send_request_with_incorrect_email
	Given User filled email '<type of mistake>' with '<data>'
	When Request sends to API 
	Then The server should return status 400
	And Response with message about incorrect email based on '<type of mistake>'

Examples: 
| type of mistake      | data                                                                                                                                                                                                                                                                    |
| without @            | exmapleEmail.com                                                                                                                                                                                                                                                        |
| too long             | xv5XfZ1LXURRkaFvIEvzp7j8Fuj16dziBW9Pv8quGJsdQfOnyKV6hosAlndp2Au244iHlJeHIaQHx2rqzcpyiwjqDywrzFz6CgCvUVVVngr2IkTfDQBsB88llpJYJWY2xbOdvLIBXQ2QOM65PlCBp0TTVQX0lBvFLIAZg7kZNM2hQIN3bpvQ2GaacERotQuF3JPwlvUUr84B9h81Y4z0MmP1hrz1bDaoAzlU5jJx3ft9dCJLXUMUgig4rDDOv@email.com |
| without data after @ | exmapleEmail@                                                                                                                                                                                                                                                           |
| without data         | empty                                                                                                                                                                                                                                                                   |

Scenario Outline: CHANGE_USER_EMAIL_[/quiz/users/email]_[PATCH]_5_send_request_with_incorrect_password
	Given User filled password '<type of mistake>' with '<data>'
	When Request sends to API 
	Then The server should return status 400
	And Response with message about incorrect password based on '<type of mistake>'

Examples: 
| type of mistake       | data                                    |
| which is too short    | short                                   |
| which is too long     | userEnteredTooLongPassword@WhichIsWrong |
| without data          | empty                                   |

Scenario: CHANGE_USER_EMAIL_[/quiz/users/email]_[PATCH]_6_send_request_without_authorization
	Given User filled correctly data without authorization
	When Request sends to API 
	Then The server should return status 403
	And Response with error message about authorization