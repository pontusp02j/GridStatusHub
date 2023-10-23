import React from 'react';
import { IconButton } from '@material-ui/core';
import EditIcon from '@material-ui/icons/Edit';
import DeleteIcon from '@material-ui/icons/Delete';
import SaveIcon from '@material-ui/icons/Save';

const GridActions = ({ grid, editingGridId, handleEdit, handleSave, handleDelete, activeGridId }) => {
    return (
        editingGridId === grid.id ? (
            <IconButton aria-label="save" onClick={(e) => handleSave(e, grid.id)}>
                <SaveIcon />
            </IconButton>
        ) : (
            <>
                {grid.id === activeGridId && editingGridId === null && (
                    <>
                        <IconButton aria-label="edit" onClick={(e) => { e.stopPropagation(); handleEdit(grid.id, grid.name); }}>
                            <EditIcon />
                        </IconButton>
                        <IconButton aria-label="delete" onClick={(e) => { e.stopPropagation(); handleDelete(grid.id); }}>
                            <DeleteIcon />
                        </IconButton>
                    </>
                )}
            </>
        )
    );
}

export default GridActions;
