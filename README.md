# WebApiProject
<h3>Api Keys:</h3> 
<li> ApiKey: "dGhpcyBpcyBteSBjb2RlLCBob3cgYWJvdXQgdGhhdA==" - To everyone - Query </li>
<li>AdminApiKey: "bGV0cyBkbyBzb21lIGVjb2RpbmcgaG93IGFib3V0IHRoYXQ==" - Only admin - Header </li>
<li>Secret: "VGhpcyBpcyBteSBzZWNyZXQga2V5IHRvIG15IGFwaQo==" - Secret after logging in - Bearer Token </li>
<li>CustomerApiKey: "dGhpcyBpcyBteSBjdXN0b21lciBrZXk==" - Only customer -Header </li>
<h3>Getting started:</h3>
To create an order: </br>
  A customer needs to exist in the database (Customer - POST) </br>
  A product needs to exist in the database (Product -  POST) </br>
  A status needs to exist in the database (Status - POST) </br>
<h3>Known bugs</h3>
When updating the price and name of a product (PUT), existings <strong>orders</strong> will also have their price and product name changed, however the line price does not reflect the change. This is a bug and not intentional. Updating a product should not make any changes in old/existing orders. <strong>orders</strong> created after the product has been edited work as intended.
