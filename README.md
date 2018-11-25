# RPC-API-SP-Endpoint
This app is an api with RPC style, the app is just a store procedure (SP) executor and the SP do a specific job (Set or Get a values from db in JSON or XML string form).
If you are from an **expert sql background** and want to build API using your SQL background, i think this app might help you figured out the way. 
Fortunately in **SQL Server 2016** or above, **SQL query can read the data inside JSON or XML string form**.

## How it Works
it's pretty simple :
⋅⋅* The client do the request to the API
```
http://localhost:1805/api/SP/Get/GetEmployee
```
⋅⋅* The API Endpoint in this app is a store procedure (SP) name that to be executed
⋅⋅* So, the syntax when call api is:
```
http://**your domain**/api/**controller**/**action**/**SP Name**
```
⋅⋅* The API only throws a value between client and SP, which means the api let the SP do the miracle
⋅⋅* So, if you want to add more specific job to be solved, you just create new SP, it's pretty simple right 

