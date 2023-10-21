import React, { useState, useContext } from 'react';
import GridContext from '../context/GridContext';
import GridControlsComp from './GridControlsComp';
import ColorsGridComp from './ColorsGridComp';
import GridItemsListComp from './GridItemsListComp';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import { validateGridName } from './../utili/GridValidations';

const GridContainerComp = () => {
  const gridStyles = {
      height: '58em',
  };
    const { createGrid, currentGrid, setCurrentGrid, grids, setActiveGridId } = useContext(GridContext); // Added grids to destructure

    const [isDialogOpen, setDialogOpen] = useState(false);
    const [gridName, setGridName] = useState('');

    const handleCreateNew = () => {
        setDialogOpen(true);
    };

    const handleDialogClose = () => {
        setDialogOpen(false);
        setGridName('');
    };

    const handleDialogSubmit = async () => {
        const error = validateGridName(gridName, grids);  
        if (error) {
            window.alert(error);
            return;
        }

        const newGrid = await createGrid({ name: gridName });
        console.log(newGrid);
        if (newGrid) {
            setCurrentGrid(newGrid);
            setActiveGridId(newGrid.id);
        }
        handleDialogClose();
    };

    return (
        <div>
            <div style={gridStyles}> 
              <ColorsGridComp gridData={currentGrid} />
            </div>
            <GridControlsComp onCreateNew={handleCreateNew} />

            <Dialog open={isDialogOpen} onClose={handleDialogClose}>
                <DialogTitle>Skapa nytt rutnät</DialogTitle>
                <DialogContent>
                    <TextField
                        autoFocus
                        margin="dense"
                        label="Grid Namn"
                        fullWidth
                        value={gridName}
                        onChange={(e) => setGridName(e.target.value)}
                        inputProps={{
                            maxLength: 15
                        }}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleDialogClose} color="primary">
                        Avbryt
                    </Button>
                    <Button onClick={handleDialogSubmit} color="primary">
                        Spara
                    </Button>
                </DialogActions>
            </Dialog>

            <GridItemsListComp />
        </div>
    );
};

export default GridContainerComp;
