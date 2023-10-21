/* 
    Run this throw administrator terminal inside infrastructure layer also run docker local
    Getting the docker id: docker ps
    cat Migrations/GridStatusHubMigration.sql | docker exec -i [CONTAINER_NAME_OR_ID] psql -U sa -d GridStatusHub
*/


DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'gridsystems') THEN

        -- Create the GridSystems table if it does not exist
        CREATE TABLE GridSystems (
            Id SERIAL PRIMARY KEY,
            Name TEXT,
            EstablishmentDate TIMESTAMP
        );
        
    END IF;

END $$;

-- Check if the GridCells table exists
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'gridcells') THEN
        
        -- Create the GridCells table if it does not exist
        CREATE TABLE GridCells (
            Id SERIAL PRIMARY KEY,
            GridSystemId INT REFERENCES GridSystems(Id),
            RowPosition INT NOT NULL,
            ColumnPosition INT NOT NULL,
            ColorStatus TEXT
        );
        
    END IF;
END $$;