/* 
    Run this throw administrator terminal inside infrastructure layer also run docker local
    Getting the docker id: docker ps
    cat Migrations/GridStatusHubMigration.sql | docker exec -i [CONTAINER_NAME_OR_ID] psql -U sa -d GridStatusHub
*/

CREATE TABLE GridSystems (
    Id SERIAL PRIMARY KEY,
    Name TEXT,
    EstablishmentDate TIMESTAMP
);

CREATE TABLE GridCells (
    Id SERIAL PRIMARY KEY,
    GridSystemId INT REFERENCES GridSystems(Id),
    RowPosition INT NOT NULL,
    ColumnPosition INT NOT NULL,
    ColorStatus TEXT
);