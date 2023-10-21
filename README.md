# Docs
Docs folder is documentation materia for this application

# Docker database
Install Docker: If you haven't already, you need to install Docker. The process is platform-dependent. You can download Docker for Mac, Windows, or Linux from Docker's official website.

Navigate to Directory: Use the terminal or command prompt to navigate to the directory containing your docker-compose.yml file.

# Run for gettiing up the database
docker-compose up

# Run migration script inside infrastructure folder
Run this throw administrator terminal inside infrastructure layer
Getting the docker id: docker ps

cat Migrations/GridStatusHubMigration.sql | docker exec -i [CONTAINER_NAME_OR_ID] psql -U sa -d GridStatusHub

# Run inside root of the project
dotnet build

# Getting Started run the backend inside presentation folder for running the backend
dotnet run

# Run inside client folder
npm install
npm start
