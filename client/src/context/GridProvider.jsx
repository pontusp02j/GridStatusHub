import React, { useState, useEffect } from 'react';
import GridAPI from '../api/GridAPI';
import GridContext from './GridContext';
import { validateGridName, sValidMessage } from './../utili/GridValidations';

const GridProvider = ({ children }) => {
    const [grids, setGrids] = useState([]);
    const [currentGrid, setCurrentGrid] = useState(null);
    const [activeGridId, setActiveGridId] = useState(null);
    const [isDataLoading, setIsDataLoading] = useState(true);

    const loadGrids = async () => {
        try 
        {
            const gridsData = await GridAPI.fetchGrids();
            
            if (!gridsData) 
            {
                window.alert("Failed to load grids data.");
                return;
            }

            const filteredGrids = gridsData.filter(grid => grid.establishmentDate && grid.establishmentDate !== '0001-01-01T00:00:00');
            setGrids(filteredGrids);
            setCurrentGrid(filteredGrids.length ? currentGrid : null);
            setIsDataLoading(false);
        } 
        catch (error)
        {
            console.error("Failed to fetch grids:", error);
            setIsDataLoading(false);        
        }
    }       

    useEffect(() => {
        loadGrids();
    }, []);

    const handleGridAction = async (actionFunc, grid) => {
        const error = validateGridName(grid.name, grids);
        if (error) {
            window.alert(error);
            return null;
        }
        return actionFunc(sValidMessage, grid);
    }

    const manageGrid = async (actionFunc, grid, gridId) => {
        const resultGrid = await handleGridAction(actionFunc, grid);
        if (resultGrid) {
            if (actionFunc === GridAPI.createGrid) {
                setActiveGridId(resultGrid.id);
            }
            loadGrids();
            return resultGrid;
        }
    }
    

    return (
        <GridContext.Provider value={{
            grids,
            currentGrid,
            activeGridId,
            isDataLoading,
            createGrid: grid => manageGrid(GridAPI.createGrid, grid),
            updateGrid: (gridId, updatedGrid) => manageGrid(GridAPI.updateGrid, updatedGrid, gridId),
            deleteGrid: async gridId => {
              await GridAPI.deleteGrid(gridId);
              setGrids(prevGrids => prevGrids.filter(grid => grid.id !== gridId));
            },
            setCurrentGrid,
            setActiveGridId,
            fetchGridData: GridAPI.fetchGridById
        }}>
            {children}
        </GridContext.Provider>
    );
}

export default GridProvider;
