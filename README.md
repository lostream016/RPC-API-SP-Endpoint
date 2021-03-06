# RPC-API-SP-Endpoint
This app is an api with RPC style, the app is just a store procedure (SP) executor and the SP do a specific job (Set or Get a values from db in JSON or XML string form).
If you are from an **expert sql background** and want to build API using your SQL background, i think this app might help you figured out the way. 
Fortunately in **SQL Server 2016** or above, **SQL query can read the data inside JSON or XML string form**.

## How it Works
it's pretty simple :
* The client do the request to the API
```
http://localhost:1805/api/SP/Get/GetEmployee?id=6
```
* The API Endpoint in this app is a store procedure (SP) name that to be executed
* So, the syntax when call api is:
```
http://**your domain**/api/**controller**/**action**/**SP Name**?**SP Param 1 = value**&**SP Param 2 = value**&...
```
* The API only throws a value between client and SP, which means the api let the SP do the miracle
* So, if you want to add more specific job to be solved, you only have to create new SP in SQL Server

## Before Run the Project
* Make sure the your DB environment is ready (**just for demo**), run the SQL Query inside **CreateTableAndData.sql file**

* And for API endpoint example, we add 2 simple SP (Get And Post), run the SQL Query inside **CreateSPEnpoint.sql file**

* Adjust the project settings with your environment <br>
-- Open **Web.config** file <br>
-- Inside **connectionStrings** XML tag <br>
-- Change **data source**=**your server name** , **initial catalog**=**your db name**, And **user id** & **password** to access the database <br>

## It's Time to Try
* Rebuild & Run the project <br>
-- You can use iis express or register your project using iis (local hosting server) to achieve this
* Open Postman desktop app or else and access the SP via API
```
METHOD GET
http://localhost:1805/api/SP/Get/GetAllEmployee
```
```
METHOD POST
http://localhost:1805/api/SP/Get/AddEmployee

REQUEST BODY (JSON)
{
	"Name": "Cassandra",
	"Age": 22,
	"FK_Job_ID": "2"
}
```

