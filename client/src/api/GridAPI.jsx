const BASE_URL = 'https://localhost:7201';
const GRID_ENDPOINT = '/gridsystems';
const HEADERS_JSON = { 'Content-Type': 'application/json' };

class GridAPI {
  
  static handleError(response, customMessage) {
    if (response.status === 400 && customMessage) {
      window.alert(customMessage);
      return null;
    }

    throw new Error(`HTTP error! Status: ${response.status}`);
  }

  static async request(endpoint, options = {}, customErrorMessage) {
    try {
      const response = await fetch(`${BASE_URL}${endpoint}`, options);

      if (response.status === 202 || options.method === 'DELETE') {
        if (!response.ok) {
          return GridAPI.handleError(response, customErrorMessage);
        }
        return;
      }

      if (response.ok) {
        return await response.json();
      } else {
        return GridAPI.handleError(response, customErrorMessage);
      }

    } catch (error) {
      console.error(`Error during ${options.method || 'GET'} request: ${error.message}`);
      if (error.message.includes("Failed to fetch") || error.message.includes("ERR_CONNECTION_REFUSED")) {
          window.alert("Network error: Failed to connect to the server. Please check your connection.");
      } else {
          window.alert("An unexpected error occurred. Please try again later.");
      }
      return null;
    }
  }

  static fetchGrids() {
    return GridAPI.request(GRID_ENDPOINT);
  }

  static fetchGridById(gridId) {
    return GridAPI.request(`${GRID_ENDPOINT}/${gridId}`);
  }

  static createGrid(errorMessage, grid) {
    return GridAPI.request(
      `${GRID_ENDPOINT}/create`,
      {
        method: 'POST',
        headers: HEADERS_JSON,
        body: JSON.stringify(grid),
      },
      errorMessage
    );
  }

  static updateGrid(errorMessage, gridSystemRequest) {
    return GridAPI.request(
      `${GRID_ENDPOINT}/Update`,
      {
        method: 'PUT',
        headers: HEADERS_JSON,
        body: JSON.stringify(gridSystemRequest),
      },
      errorMessage
    );
  }

  static deleteGrid(gridId) {
    return GridAPI.request(`${GRID_ENDPOINT}/delete/${gridId}`, { method: 'DELETE' });
  }
}

export default GridAPI;
