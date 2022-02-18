# WebApiProject
<h3>Api Keys:</h3> 
  * "ApiKey": "dGhpcyBpcyBteSBjb2RlLCBob3cgYWJvdXQgdGhhdA==" - To everyone - Query </br>
  * "AdminApiKey": "bGV0cyBkbyBzb21lIGVjb2RpbmcgaG93IGFib3V0IHRoYXQ==" - Only admin - Header </br>
  * "Secret": "VGhpcyBpcyBteSBzZWNyZXQga2V5IHRvIG15IGFwaQo==" - Secret after logging in - Bearer Token </br>
  * "CustomerApiKey": "dGhpcyBpcyBteSBjdXN0b21lciBrZXk==" - Only customer -Header </br>
<h3>Getting started:</h3>
To create an order: </br>
  A customer needs to be in the database (Customer - POST) </br>
  A product needs to be in the database (Product -  POST) </br>
  A status needs to be in the database (Status - POST) </br>
</br>
</br>
Known bugs</br>
When updating the price and name of a product (PUT), existings **orders** will also have their price and product name changed, however the line price does not reflect the change. This is a bug and not intentional. Updating a product should not make any changes in old/existing orders
