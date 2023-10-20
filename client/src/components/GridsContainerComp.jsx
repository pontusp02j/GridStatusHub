import React, { useState, useContext } from 'react';
import GridContext from '../context/GridContext';
import GridControlsComp from './GridControlsComp';
import ColorsGridComp from './ColorsGridComp';
import GridItemsListComp from './GridItemsListComp' 
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';


const GridContainerComp = () => {
  const { createGrid, currentGrid, fetchGridData } = useContext(GridContext);
  
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
    await createGrid({ name: gridName });
    handleDialogClose();
  };

  return (
    <div>
      <ColorsGridComp gridData={currentGrid} />
      <GridControlsComp onCreateNew={handleCreateNew} />
      
      <Dialog open={isDialogOpen} onClose={handleDialogClose}>
        <DialogTitle>Create New Grid</DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            margin="dense"
            label="Grid Name"
            fullWidth
            value={gridName}
            onChange={(e) => setGridName(e.target.value)}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleDialogClose} color="primary">
            Cancel
          </Button>
          <Button onClick={handleDialogSubmit} color="primary">
            Submit
          </Button>
        </DialogActions>
      </Dialog>

      <GridItemsListComp />
    </div>
  );
};

export default GridContainerComp;