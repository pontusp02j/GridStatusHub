import React, { useContext } from 'react';
import GridContext from './../../context/GridContext';
import ColorBlock from './ColorBlock';
import ErrorMessage from './ErrorMessage';
import useStyles from './styles';

const ColorsGridComp = ({ gridData }) => {
    const gridCells = gridData?.gridCells || [];
    const classes = useStyles();
    const { updateGrid, setCurrentGrid, fetchGridData } = useContext(GridContext);

    const handleColorBlockClick = async (cellId) => {
        try {
            const gridSystemRequest = {
                Id: gridData.id,
                GridCells: [gridCells.find(cell => cell.id === cellId)]
            };
            
            await updateGrid(gridSystemRequest.Id, gridSystemRequest);
            await new Promise(resolve => setTimeout(resolve, 10));
            const updatedGridData = await fetchGridData(gridSystemRequest.Id);    
            setCurrentGrid(updatedGridData);
        
        } catch (error) {
            console.error("Error updating the grid:", error);
        }
    };

    return (
        <>
            {gridData && <p>Rutnätsnamn : {gridData.name}</p>}
            <div className={classes.gridContainer}>
                {(!gridData || !gridCells || gridCells.length === 0) ? (
                    <ErrorMessage />
                ) : (
                    gridCells.map((cell) => (
                        <ColorBlock 
                            key={cell.id}
                            cell={cell}
                            onClick={() => handleColorBlockClick(cell.id)}
                        />
                    ))
                )}
            </div>
        </>
    );
};

ColorsGridComp.defaultProps = {
    gridData: { gridCells: [] }
};

export default ColorsGridComp;
