import React, { useContext, useState, useEffect } from 'react';
import {
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow
} from '@material-ui/core';
import GridContext from '../../context/GridContext';
import GridRow from './GridRow';
import useStyles from './styles'

const GridItemsListComp = ({ editingGridId, setEditingGridId, currentGridId }) => {
    const classes = useStyles();
    const { grids, currentGrid, updateGrid, deleteGrid, setCurrentGrid, fetchGridData } = useContext(GridContext);

    const [activeGridId, setActiveGridId] = useState(null);
    const [editedName, setEditedName] = useState("");

    useEffect(() => {
        setActiveGridId(currentGridId);
    }, [currentGridId]);

    const handleRowClick = async (grid) => {
        const latestGridData = await fetchGridData(grid.id);
        setCurrentGrid(latestGridData);
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
                        {grids.filter(grid => grid.establishmentDate !== '0001-01-01T00:00:00')
                            .map((grid) => (
                                <GridRow
                                    key={grid.id}
                                    grid={grid}
                                    activeGridId={activeGridId}
                                    editingGridId={editingGridId}
                                    editedName={editedName}
                                    setEditedName={setEditedName}
                                    handleRowClick={handleRowClick}
                                    handleEdit={handleEdit}
                                    handleSave={handleSave}
                                    handleDelete={handleDelete}
                                />
                            ))}
                    </TableBody>
                </Table>
            </TableContainer>
        ) : null
    );
};

export default GridItemsListComp;
