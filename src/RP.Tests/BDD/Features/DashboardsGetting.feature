Feature: DashboardsGetting
Background: 
	Given I do not have any created dashboards

@nonParallel
Scenario: Verify dashboard creation amount
	Given I have created dashboards
		| Name        | Description       |
		| asdfDBName  | NewDBDescription  |
		| NewDBName21 | NewDBDescription1 |
		| Name13      | Description1      |
	When I get created dashboards
	Then I make sure that dashboards amount is 3
