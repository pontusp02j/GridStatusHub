import React from 'react';
import { TableRow, TableCell, TextField } from '@material-ui/core';
import GridActions from './GridActions';

const GridRow = ({ grid, activeGridId, editingGridId, editedName, setEditedName, handleRowClick, handleEdit, handleSave, handleDelete }) => {
    return (
        <TableRow
            key={grid.id}
            onClick={editingGridId ? null : () => handleRowClick(grid)}
            className={`${editingGridId === grid.id ? 'editing' : ''} ${grid.id === activeGridId ? 'active' : ''}`}>
            <TableCell component="th" scope="row">
                {editingGridId === grid.id ? (
                    <TextField
                        autoFocus
                        margin="dense"
                        label="Namn"
                        fullWidth
                        value={editedName}
                        onChange={(e) => setEditedName(e.target.value)}
                        inputProps={{
                            maxLength: 15,
                            pattern: "^[a-zA-Z0-9]*$"
                        }}
                    />
                ) : (
                    grid.name
                )}
            </TableCell>
            <TableCell align="right">
            <GridActions 
                grid={grid} 
                editingGridId={editingGridId} 
                activeGridId={activeGridId} 
                handleEdit={handleEdit}
                handleSave={handleSave}
                handleDelete={handleDelete}
            />
            </TableCell>
        </TableRow>
    );
}

export default GridRow;
