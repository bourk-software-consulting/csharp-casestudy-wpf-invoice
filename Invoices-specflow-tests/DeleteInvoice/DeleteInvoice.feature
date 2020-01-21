Feature: DeleteInvoice
    as an invoice manager
	I would like to remove invoice from the list

@mytag
Scenario: Delete an Invoice
	Given I have an existing invoice
	When I press delete on this invoice
	Then the invoice should not appear in the list
