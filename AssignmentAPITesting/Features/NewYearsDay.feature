Feature: NewYearsDay
	Verify that API returned new year’s day for the Netherlands

@api
Scenario Outline: Call Public holiday end point and check new year's day for the NL last and next 5 year
	Given I have the end point of the nager api services
	When I make a request call with "<Year>" and "<Country Code>"
	Then the ResponseStatus is OK
	And the response should give me New Year's day and validate the "<Date>" Localname and Name accordingly
	Examples: 
	| Year | Country Code | Date       | 
	| 2017 | NL           |	2017-01-01 | 
	| 2018 | NL           |	2018-01-01 | 
	| 2019 | NL           |	2019-01-01 | 
	| 2020 | NL           |	2020-01-01 | 
	| 2021 | NL           |	2021-01-01 | 
	| 2021 | NL           |	2021-01-01 | 
	| 2022 | NL           |	2022-01-01 | 
	| 2023 | NL           |	2023-01-01 | 
	| 2024 | NL           |	2024-01-01 | 
	| 2025 | NL           |	2025-01-01 | 

@api
Scenario Outline: Call Public holiday end point and check Hemelvaartsdag is always on Thursday
	Given I have the end point of the nager api services
	When I make a request call with "<Year>" and "<Country Code>"
	Then the ResponseStatus is OK
	And the response should give me "Hemelvaartsdag" and validate is the weekday name "Thursday" accordingly
	Examples: 
	| Year | Country Code | Date       | 
	| 2021 | NL           |	2021-01-01 | 
	| 2022 | NL           |	2022-01-01 | 
	| 2023 | NL           |	2023-01-01 | 
	| 2024 | NL           |	2024-01-01 | 
	| 2025 | NL           |	2025-01-01 | 