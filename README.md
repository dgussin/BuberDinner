# BuberDiner sample app
Sample application to explore DDD and clean architectur

## To run the app
After cloning the app, set the `JwtSettings:Secret` with the value of `super-secret-key` in the `appsettings.Development.json` file.  Or you can run the following command from a terminal:
```shell
dotnet user-secrets --project .\BuberDinner.Api\ set JwtSettings:Secret super-secret-key
```
## Sample Request
Request can be found in the `\Requests` folder..
