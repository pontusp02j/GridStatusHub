const BASE_URL = 'https://localhost:7201';

class GridAPI {
    static async handleResponse(response) {
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        try {
            return await response.json();
        } catch (error) {
            throw new Error('The response is not valid JSON.');
        }
    }

    static logError(action, error) {
        if (error.message === 'Failed to fetch') {
            console.error(`Unable to ${action}. Please check your network connection or ensure the server is running.`);
        } else {
            console.error(`Error during ${action}:`, error.message);
        }
    }

    static async fetchGrids() {
      try {
        const response = await fetch(`${BASE_URL}/gridsystems`);
        return await this.handleResponse(response);
      } catch (error) {
        this.logError('fetch grids', error);
        return [];
      }
    }
    
    static async fetchGridById(gridId) {
      try {
          const response = await fetch(`${BASE_URL}/gridsystems/${gridId}`);
          return await this.handleResponse(response);
      } catch (error) {
          this.logError('fetch grid by ID', error);
          return null;
      }
    }

    static async createGrid(grid) {
      try {
        const response = await fetch(`${BASE_URL}/gridsystem/create`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(grid),
        });
    
        if (response.ok) {
          return await response.json();
        } else if (response.status === 400) {
          const errorData = await response.json();

          if (errorData.message) {
            window.alert("Du får enbart lägga in bokstäver och siffror och samma namn på rutnätetna får inte existera");
          } else {
            window.alert("Bad request.");
          }
          
          return null;
        } else {
          window.alert("An error occurred while creating the grid.");
          return null;
        }
      } catch (error) {
        this.logError('create grid', error);
        return null;
      }
    }
    

    static async updateGrid(gridSystemRequest) {
      try {
          const response = await fetch(`${BASE_URL}/gridsystem/Update`, {
            method: 'PUT',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify(gridSystemRequest),
          });

          if(response.status == 202) {
            return;
          }

          return await this.handleResponse(response);

      } catch (error) {
        window.alert("Du får enbart lägga in bokstäver och siffror och samma namn på rutnätetna får inte existera: " + error);
        return null;
      }
  }
   
  static async deleteGrid(gridId) {
    try {
      const response = await fetch(`${BASE_URL}/gridsystem/delete/${gridId}`, { method: 'DELETE' });
      if (!response.ok) {
          throw new Error(`Failed to delete grid. HTTP Status: ${response.status}`);
      }
    } catch (error) {
      this.logError('delete grid', error);
    }
  }
}

export default GridAPI;
