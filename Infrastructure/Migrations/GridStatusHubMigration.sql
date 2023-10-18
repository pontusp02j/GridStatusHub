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
    
    -- Insert mock data into GridSystems only if it's empty
    IF NOT EXISTS (SELECT 1 FROM GridSystems) THEN
        INSERT INTO GridSystems (Name, EstablishmentDate)
        VALUES ('Grid System 1', NOW()),
               ('Grid System 2', NOW());
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

    -- Insert mock data into GridCells associated with the first grid system only if it's empty
    IF NOT EXISTS (SELECT 1 FROM GridCells WHERE GridSystemId = 1) THEN
        INSERT INTO GridCells (GridSystemId, RowPosition, ColumnPosition, ColorStatus)
        VALUES (1, 1, 1, 'gray'),
               (1, 1, 2, 'green'),
               (1, 1, 3, 'orange'),
               (1, 2, 1, 'red'),
               (1, 2, 2, 'gray'),
               (1, 2, 3, 'green');
    END IF;
    
    -- Insert mock data into GridCells associated with the second grid system only if it's empty
    IF NOT EXISTS (SELECT 1 FROM GridCells WHERE GridSystemId = 2) THEN
        INSERT INTO GridCells (GridSystemId, RowPosition, ColumnPosition, ColorStatus)
        VALUES (2, 1, 1, 'green'),
               (2, 1, 2, 'green'),
               (2, 1, 3, 'orange'),
               (2, 2, 1, 'red'),
               (2, 2, 2, 'orange'),
               (2, 2, 3, 'gray');
    END IF;

END $$;
