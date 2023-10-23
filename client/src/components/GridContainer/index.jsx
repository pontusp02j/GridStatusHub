import React, { useState, useContext } from 'react';
import GridContext from '../../context/GridContext';
import ColorsGridComp from './../GridCellsColor/index';
import GridItemsListComp from './../GridItemsList/index';
import GridControlsComp from './GridControlsComp';
import useGridDialog from './useGridDialog';
import CreateGridDialog from './CreateGridDialog';

const gridStyles = {
    height: '58em',
};

const GridContainerComp = () => {
    const { createGrid, currentGrid, setCurrentGrid, grids, setActiveGridId } = useContext(GridContext);
    const [editingGridId, setEditingGridId] = useState(null);

    const { isDialogOpen, gridName, handleCreateNew, handleDialogClose, handleDialogSubmit, setGridName } = 
        useGridDialog('', grids, createGrid, setCurrentGrid, setActiveGridId);

    return (
        <div>
            <div style={gridStyles}> 
                <ColorsGridComp gridData={currentGrid} />
            </div>
            <GridControlsComp onCreateNew={handleCreateNew} editingGridId={editingGridId} />
            <CreateGridDialog
                open={isDialogOpen}
                onClose={handleDialogClose}
                onSubmit={handleDialogSubmit}
                gridName={gridName}
                setGridName={setGridName}
            />
            <GridItemsListComp
                editingGridId={editingGridId} 
                setEditingGridId={setEditingGridId} 
                currentGridId={currentGrid?.id}
            />
        </div>
    );
};

export default GridContainerComp;
