Feature: Change user password

Background: 
	Given User signs up in system
	And User signs in
	
Scenario: CHANGE_USER_PASSWORD_1_[/quiz/users/password]_[PUT]_send_request_with_correct_data
	Given User filled data correctly
	When Request sends to API 
	Then The server should return status 200
	And Response with success message

Scenario: CHANGE_USER_PASSWORD_2_[/quiz/users/password]_[PUT]_send_request_with_empty_data
	Given User didn't fill data
	When Request sends to API 
	Then The server should return status 400
	And Response with message about missing data

Scenario: CHANGE_USER_PASSWORD_3_[/quiz/users/password]_[PUT]_send_request_with_password_which_is_not_assigned_to_account
	Given User filled password which is incorrect 
	When Request sends to API 
	Then The server should return status 400
	And Response with message about password 

Scenario Outline: CHANGE_USER_PASSWORD_4_[/quiz/users/password]_[PUT]_send_request_with_incorrect_new_password
	Given User filled new password '<type of mistake>' with '<data>'
	When Request sends to API 
	Then The server should return status 400
	And Response with message about incorrect new password

	Examples: 
		| type of mistake       | data                                    |
		| which is too short    | short                                   |
		| which is too long     | userEnteredTooLongPassword@WhichIsWrong |
		| without data          |                                         |
		| without special key   | password                                |
		| which is not a string | int                                     |

Scenario: CHANGE_USER_PASSWORD_5_[/quiz/users/password]_[PUT]_send_request_with_confirm_password_which_is_different_from_password
	Given User filled confirm password incorrect 
	When Request sends to API 
	Then The server should return status 400
	And Response with message about confirm password 


Scenario Outline: CHANGE_OF_USER_PASSWORD_6_[/quiz/users/password]_[PUT]_send_request_with_incorrect_password
	Given User filled password '<type of mistake>' with '<data>'
	When Request sends to API 
	Then The server should return status 400
	And Response with message about incorrect password

	Examples: 
		| type of mistake       | data                                    |
		| which is too short    | short                                   |
		| which is too long     | userEnteredTooLongPassword@WhichIsWrong |
		| without data          |                                         |
		| without special key   | password                                |
		| which is not a string | float                                   |