import React, { useContext, useState, useEffect } from 'react';
import {
    makeStyles,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    IconButton,
    TextField
} from '@material-ui/core';
import EditIcon from '@material-ui/icons/Edit';
import DeleteIcon from '@material-ui/icons/Delete';
import SaveIcon from '@material-ui/icons/Save';
import GridContext from '../context/GridContext';

const useStyles = makeStyles((theme) => ({
    activeRow: {
        backgroundColor: theme.palette.action.selected,
    },
    tableContainer: {
        marginTop: theme.spacing(10),
        marginBottom: theme.spacing(10),
        maxWidth: 650,
        margin: '0 auto',
        border: '1px solid rgba(128, 128, 128, 0.3)'
    },
    tableRow: {
        height: '100px',
        cursor: 'pointer',
        '&.editing': {
            cursor: 'default',
        },
        '&:not(.editing):hover': {
            backgroundColor: theme.palette.action.hover,
        }
    },
}));

const GridItemsListComp = ({ editingGridId, setEditingGridId, currentGridId  }) => {
    const classes = useStyles();
    const { grids, currentGrid, updateGrid, deleteGrid, setCurrentGrid, fetchGridData } = useContext(GridContext);

    const [activeGridId, setActiveGridId] = useState(null);
    const [editedName, setEditedName] = useState("");

    useEffect(() => 
    {
        ((grids.length > 0 && activeGridId === null)) ? 
            setActiveGridId(grids[0].id) : setActiveGridId(currentGridId);

    }, [grids, activeGridId, currentGridId]);

    const handleRowClick = async (grid) => {
        const latestGridData = await fetchGridData(grid.id);
        setCurrentGrid(latestGridData);
        setActiveGridId(grid.id);
    };

    const handleEdit = (gridId, gridName) => {
        if (editingGridId) return;
        
        setEditingGridId(gridId);
        setEditedName(gridName);
    };

    const handleSave = async (e, gridId) => {
        e.stopPropagation();
        if (editedName.trim()) {
            const { gridCells, ...gridWithoutCells } = currentGrid;
            await updateGrid(gridId, { ...gridWithoutCells, name: editedName });
            setCurrentGrid(currentGrid);
            setEditingGridId(null);
            setEditedName("");
        }
    };

    const handleDelete = (gridId) => {
        deleteGrid(gridId);
        if (gridId === currentGrid.id) {
            setCurrentGrid(null);
            setActiveGridId(null);
        }
    };

    return (
        grids && grids.length > 0 ? (
            <TableContainer className={classes.tableContainer}>
                <Table aria-label="grid table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Namn</TableCell>
                            <TableCell align="right">Ta Bort / Editera</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {grids.filter(grid => grid.establishmentDate != null
                            && grid.establishmentDate != null && grid.establishmentDate !== '0001-01-01T00:00:00')
                            .map((grid, index) => (
                                <TableRow
                                    key={`${grid.id}-${index}`}
                                    onClick={editingGridId ? null : () => handleRowClick(grid)}
                                    className={`${classes.tableRow} ${grid.id === activeGridId ? classes.activeRow : ''} ${editingGridId === grid.id ? 'editing' : ''}`}>

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
                                        {editingGridId === grid.id ? (
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
                                        )}
                                    </TableCell>
                                </TableRow>
                            ))}
                    </TableBody>
                </Table>
            </TableContainer>
        ) : null
    );
};

export default GridItemsListComp;
