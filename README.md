
# HereInAfter Library API Documentation

## Setup and Run Locally

To run this project locally, follow the steps below:

1. Clone the repository:

   git clone https://github.com/your-username/hereinafter-library.git

2. Navigate to the project directory:

   cd hereinafter-library

3. Install dependencies:

   composer install

4. Create a copy of the `.env.example` file and rename it to `.env`:

   ```bash
   cp .env.example .env
   ```

5. Generate an application key:

   ```bash
   php artisan key:generate
   ```

6. Configure your database settings in the `.env` file.

7. Run database migrations:

   ```bash
   php artisan migrate
   ```

8. Seed the database (if needed):

   ```bash
   php artisan db:seed
   ```

9. Start the development server:

   ```bash
   php artisan serve
   ```

10. The API will be available at `http://127.0.0.1:8000` by default.

## API Endpoints

## Delete Book

Delete a book from the library.

### Request

- **URL:** `http://127.0.0.1:7600/api/book/2`
- **Method:** `DELETE`
- **Authorization:** Bearer Token
- **Token:** `<token>`

### Response

- Status: `204 No Content`

---

## Get User Token

Get the user token for authentication.

### Request

- **URL:** `http://127.0.0.1:7600/api/test/get-token/6`
- **Method:** `GET`

### Response

- Status: `200 OK`
- Body: `<token>`

---

## Add Book

Add a new book to the library.

### Request

- **URL:** `http://127.0.0.1:7600/api/book`
- **Method:** `POST`
- **Authorization:** Bearer Token
- **Token:** `<token>`
- **Body:** form-data
  - `name`: Test Book 3
  - `author_ids`: 5,7

### Response

- Status: `201 Created`
- Body: JSON representation of the newly created book

---

## Modify Book

Modify details of a book in the library.

### Request

- **URL:** `http://127.0.0.1:7600/api/book/3`
- **Method:** `PUT`
- **Authorization:** Bearer Token
- **Token:** `<token>`
- **Query Params:**
  - `name`: Test

### Response

- Status: `200 OK`
- Body: JSON representation of the modified book
