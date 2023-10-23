import React, { useContext, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import GridContext from './../context/GridContext';

const useStyles = makeStyles((theme) => ({

  title: {
    textAlign: 'center',
    margin: `${theme.spacing(2)}px 0`,
    fontSize: '1.5em',
  },

  gridContainer: {
    display: 'grid',
    gridTemplateColumns: 'repeat(5, 1fr)',
    gridTemplateRows: 'repeat(5, 1fr)',
    gap: theme.spacing(1),
    margin: `${theme.spacing(12)}px auto`,
    boxShadow: '0px 0px 10px rgba(0, 0, 0, 0.5)',
    padding: theme.spacing(1),
  },
  
  gridItem: {
    width: '100%',
    position: 'relative',
    border: '1px solid #aaa',
    boxShadow: '0px 5px 15px rgba(0, 0, 0, 0.2)',
    marginBottom: theme.spacing(1),
    transition: 'transform 0.3s ease, box-shadow 0.3s ease',
    cursor: 'pointer',
    '&:hover': {
      transform: 'scale(1.05)',
      boxShadow: '0px 8px 20px rgba(0, 0, 0, 0.3)',
    }

  },

  colorBlock: {
    width: '100%', 
    height: '8em', 
    borderBottom: '5px solid',
    background: (props) => `linear-gradient(45deg, ${props.color} 0%, ${props.color} 55%, #fff 75%)`,
  },

  errorBox: {
    position: 'absolute',
    left: '0%',
    top: '5em',
    width: '100%',
    padding: '2em',
    textAlign: 'center',
    borderRadius: theme.shape.borderRadius * 1,
  },
}));

const ColorsGridComp = ({ gridData }) => {
  const gridCells = gridData?.gridCells || [];
  const classes = useStyles({ totalCells: gridCells ? gridCells.length : 0 });
  const { updateGrid, setCurrentGrid, fetchGridData } = useContext(GridContext);

  const handleColorBlockClick = async (cellId) => 
  {
    const gridSystemRequest = {
      Id: gridData.id,
      GridCells: [gridCells.find(cell => cell.id === cellId)]
    };
    
    await updateGrid(gridSystemRequest.Id, gridSystemRequest);
    
    const updatedGridData = await fetchGridData(gridSystemRequest.Id);
    setCurrentGrid(updatedGridData);
  }
  
  return (
    <>
        {gridData && <p>Rutnätsnamn : {gridData.name}</p>}
        <div className={classes.gridContainer}>
            {(!gridData || !gridCells || gridCells.length === 0) ? (
                <Paper className={classes.errorBox} elevation={3}>
                    <p>Klicka på en av namnen för att få upp rutnätet, det går även att byta rutnät mellan de.</p>
                </Paper>
            ) : (
                gridCells.map((cell) => {
                    const gradientBackground = `linear-gradient(45deg, ${cell.colorstatus} 0%, ${cell.colorstatus} 55%, #fff 75%)`;
                    return (
                        <Paper 
                            key={cell.id} 
                            className={classes.gridItem} 
                            elevation={3}
                            onClick={() => handleColorBlockClick(cell.id)}
                            style={{ 
                                gridRow: cell.rowposition,
                                gridColumn: cell.columnposition
                            }}
                        >
                            <div className={classes.colorBlock} style={{ background: gradientBackground, borderBottomColor: cell.colorstatus }}></div>
                        </Paper>
                    );
                })
            )}
        </div>
    </>
  );
};

ColorsGridComp.defaultProps = {
  gridData: { gridCells: [] }
};

export default React.memo(ColorsGridComp);
