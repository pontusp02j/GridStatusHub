import { useState, useCallback } from 'react';
import { validateGridName } from './../../utili/GridValidations';

const useGridDialog = (initialGridName, grids, createGrid, setCurrentGrid, setActiveGridId) => {
    const [isDialogOpen, setDialogOpen] = useState(false);
    const [gridName, setGridName] = useState(initialGridName);

    const handleCreateNew = useCallback(() => setDialogOpen(true), []);
    const handleDialogClose = useCallback(() => {
        setDialogOpen(false);
        setGridName(initialGridName);
    }, [initialGridName]);

    const handleDialogSubmit = useCallback(async () => {
        const error = validateGridName(gridName, grids);
        
        if (error) 
        {
            window.alert(error);
            return;
        }

        await createGrid({ name: gridName });

        handleDialogClose();
    }, [gridName, grids, createGrid, setCurrentGrid, setActiveGridId, handleDialogClose]);

    return { isDialogOpen, gridName, handleCreateNew, handleDialogClose, handleDialogSubmit, setGridName };
};

export default useGridDialog;