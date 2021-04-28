Feature: Login module

Scenario: LOGIN_MODULE_[quiz/users/login]_1_send request_with_correctly_data
	Given User registers in the system
	And User filled email and password correctly
	When Request sends to API
	Then The server should return status 200 
	And Response should return token

Scenario: LOGIN_MODULE_[quiz/users/login]_2_send request_with_empty_data
	Given User registers in the system
	And User didn't fill email and password
	When Request sends to API 
	Then The server should return status 400
	And Response with error about missing data

Scenario Outline: LOGIN_MODULE_[quiz/users/login]_3_send_request_with_incorrect_<field>
	Given User registers in the system
	And User fills incorrect <field>
	When Request sends to API 
	Then The server should return status 400
	And Response with error about incorrect <field>

Examples: 
	| field       |
	| Adres email |
	| Hasło       |

Scenario Outline: LOGIN_MODULE_[/quiz/users/login]_[POST]_4_send_request_with_data_which_is_too_short
	Given User filled too short <field>
	When Request sends to API
	Then The server should return status 400
	And Response with error about too short <field>

Examples: 
	| field       |
	| Adres email |
	| Hasło       |

Scenario Outline: LOGIN_MODULE_[/quiz/users/login]_[POST]_5_send_request_with_data_which_is_too_long
	Given User filled too long <field>
	When Request sends to API
	Then The server should return status 400
	And Response with error about too long <field>

Examples: 
	| field       |
	| Adres email |
	| Hasło       |