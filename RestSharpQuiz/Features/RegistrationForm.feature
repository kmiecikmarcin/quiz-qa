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
	And Response with error about missing data

Scenario Outline: REGISTRATION_FORM_[/quiz/users/register]_[POST]_3_send_request_with_incorrect_<field>
	Given Users filled data with incorrect <field>
	When Request sends to API
	Then The server should return status 400
	And Response with error about incorrect <field>

Examples: 
	| field       |
	| Adres email |
	| Hasło       |
	| Płeć        |
	| Weryfikacja |

Scenario Outline: REGISTRATION_FORM_[/quiz/users/register]_[POST]_4_send_request_without_data_in_<field>
	Given User didn't fill data in <field>
	When Request sends to API
	Then The server should return status 400
	And Response with error about empty <field>

Examples: 
	| field         |
	| Adres email   |
	| Hasło         |
	| Powtórz hasło |
	| Płeć			|
	| Weryfikacja	|

Scenario Outline: REGISTRATION_FORM_[/quiz/users/register]_[POST]_5_send_request_with_data_which_is_too_short
	Given User filled too short <field>
	When Request sends to API
	Then The server should return status 400
	And Response with error about too short <field>

Examples: 
	| field       |
	| Adres email |
	| Hasło       |
	| Płeć        |

Scenario Outline: REGISTRATION_FORM_[/quiz/users/register]_[POST]_6_send_request_with_data_which_is_too_long
	Given User filled too long <field>
	When Request sends to API
	Then The server should return status 400
	And Response with error about too long <field>

Examples: 
	| field       |
	| Adres email |
	| Hasło       |
	| Płeć        |