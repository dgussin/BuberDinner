# Buber Dinner API

- [Buber Dinner API](#buber-dinner-api)
  - [Auth](#auth)
    - [Register](#register)
      - [Register Request](#register-request)
      - [Register Response](#register-response)
    - [Login](#login)
      - [Login Request](#login-request)
      - [Login Response](#login-response)
  
## Auth

### Register
```js
POST {(host)}/auth/register
```

#### Register Request

```json
{
  "firstName" : "Amichai",
  "LastName": "Mantinband",
  "email": "amichai@mantinband.com",
  "password": "Amiko1232!"
}
```

#### Register Response
```js
200 OK
```

```json
{
  "id": "d89c2d9a-eb3e-4075-95ff-b920b55aa104",
  "firstName": "Amichai",
  "lastName" : "Mantinband",
  "email": "amichai@mantinband.com",
  "token": "eyJhb.. hbbQ"
}
```

### Login
```js
POST {(host)}/auth/login
```

#### Login Request

```json
{
  "email": "amichai@mantinband.com",
  "password": "Amiko1232!"
}
```

#### Login Response
```js
200 OK
```

```json
{
  "id": "d89c2d9a-eb3e-4075-95ff-b920b55aa104",
  "firstName": "Amichai",
  "lastName" : "Mantinband",
  "email": "amichai@mantinband.com",
  "token": "eyJhb.. hbbQ"
}
```
