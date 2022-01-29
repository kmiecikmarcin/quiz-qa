Feature: Change user password

Background: 
	Given User registers into system and logs in
	
Scenario: CHANGE_USER_PASSWORD_[/quiz/users/password]_[PATCH]_1_send_request_with_correct_data
	Given User filled data correctly
	When Request sends to API 
	Then The server should return status 200 on success
	And Response with a new token

Scenario: CHANGE_USER_PASSWORD_[/quiz/users/password]_[PATCH]_2_send_request_with_empty_data
	Given User didn't fill data
	When Request sends to API 
	Then The server should return status 400
	And Response with error message about missing data

Scenario: CHANGE_USER_PASSWORD_[/quiz/users/password]_[PATCH]_3_send_request_with_password_which_is_not_assigned_to_account
	Given User filled password which is not assigned to account 
	When Request sends to API 
	Then The server should return status 400
	And Response with error message about wrong password 

Scenario Outline: CHANGE_USER_PASSWORD_[/quiz/users/password]_[PATCH]_4_send_request_with_incorrect_new_password
	Given User filled new password '<type of mistake>' with '<data>'
	When Request sends to API 
	Then The server should return status 400
	And Response with error message about incorrect the new password based on '<type of mistake>'

Examples: 
	| type of mistake       | data                                    |
	| too short             | short                                   |
	| too long              | userEnteredTooLongPassword@WhichIsWrong |
	| without data          | empty                                   |
	| without special key   | password                                |

Scenario: CHANGE_USER_PASSWORD_[/quiz/users/password]_[PATCH]_5_send_request_with_confirm_password_which_is_different_from_password
	Given User filled confirm password field incorrect 
	When Request sends to API 
	Then The server should return status 400
	And Response with error message about wrong confirmation


Scenario Outline: CHANGE_USER_PASSWORD_[/quiz/users/password]_[PATCH]_6_send_request_with_incorrect_password
	Given User filled password '<type of mistake>' with '<data>'
	When Request sends to API 
	Then The server should return status 400
	And Response with error message about incorrect password based on '<type of mistake>'

Examples: 
	| type of mistake       | data                                    |
	| too short             | short                                   |
	| too long              | userEnteredTooLongPassword@WhichIsWrong |
	| without data          | empty                                   |
	| without special key   | password                                |
