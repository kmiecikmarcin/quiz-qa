Feature: Registration form

Scenario: REGISTRATION_FORM_[/quiz/users/register]_[POST]_1_send_request_with_correctly_data
	Given User filled data correctly
	When Request sends to API 
	Then The server should return positive status 200 
	And Response with message about successfully process

Scenario: REGISTRATION_FORM_[/quiz/users/register]_[POST]_2_send_request_with_empty_form
	Given User didn't fill data
	When Request sends to API 
	Then The server should return status 400
	And JSON body with message about missing data