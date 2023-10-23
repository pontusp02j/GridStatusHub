import React from 'react';
import { Dialog, DialogActions, DialogContent, DialogTitle, Button, TextField } from '@mui/material';

const CreateGridDialog = ({ open, onClose, onSubmit, gridName, setGridName }) => (
    <Dialog open={open} onClose={onClose}>
        <DialogTitle>Skapa nytt rutnät</DialogTitle>
        <DialogContent>
            <TextField
                autoFocus
                margin="dense"
                label="Grid Namn"
                fullWidth
                value={gridName}
                onChange={(e) => setGridName(e.target.value)}
                inputProps={{ maxLength: 15 }}
            />
        </DialogContent>
        <DialogActions>
            <Button onClick={onClose} color="primary">Avbryt</Button>
            <Button onClick={onSubmit} color="primary">Spara</Button>
        </DialogActions>
    </Dialog>
);

export default CreateGridDialog;
