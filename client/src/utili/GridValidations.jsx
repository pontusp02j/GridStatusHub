const sValidMessage = "Du får enbart lägga in bokstäver och siffror samt inte ha samma namn som ett befintligt";

const validateGridName = (name, existingGrids) => {
    const pattern = /^[a-zA-Z0-9]*$/;
    if (!pattern.test(name)) 
    {
      return sValidMessage;
    }
  
    if (existingGrids.some(grid => grid.name === name)) 
    {
      return sValidMessage;
    }
  
    return null;
  };

export { validateGridName, sValidMessage };