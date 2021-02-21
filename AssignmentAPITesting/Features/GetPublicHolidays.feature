Feature: GetPublicHoliday
	As a web servies user I should get public holidays information

@api
Scenario: Call Public holiday end point and check response 
	Given I have the end point of the nager api services
	When I make a request call with "2021" and "NL"
	Then the ResponseStatus is Completed
	And the response should give me national holiday results
	| Year | Country Code | Date       | localName     | name           | fixed | global | launchYear | type   |
	| 2021 | NL           | 2021-01-01 | Nieuwjaarsdag | New Year's Day | true  | true   | 1967       | Public |

	
@api
Scenario: Call Public holiday end point and check response for "Bevrijdingsdag" and validate the type
	Given I have the end point of the nager api services
	When I make a request call with "2021" and "NL"
	Then the ResponseStatus is Completed
	And the response should give me "Bevrijdingsdag" holiday and type "School, Authorities" results

