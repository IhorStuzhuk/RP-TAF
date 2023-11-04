Feature: Dashboards

Rule: Dashboard creation
Scenario Outline: Verify dashboard creation
	Given I have created dashboard with <name> and <description>
	When I get created dashboard
	Then I make sure that dashboard has <name> and <description>
Examples:
	| name       | description       |
	| AdsaName   | NewDBDescription  |
	| NewDBName1 | NewDBDescription1 |
	| Name1      | Description1      |

Scenario: Verify dashboard editing
	Given I have created dashboard
		| Field       | Value            |
		| Name        | NewDBName        |
		| Description | NewDBDescription |
	When I edit last created dashboard with new fields
		| Field       | Value               |
		| Name        | EditedDBName        |
		| Description | EditedDBDescription |
	Then I make sure that dashboard is edited and has the following fields:
		| Field       | Value               |
		| Name        | EditedDBName        |
		| Description | EditedDBDescription |