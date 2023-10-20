// dbHelper.js

import { openDB } from 'idb';

const DB_NAME = 'grid-database';
const STORE_NAME = 'grids';

async function getDB() {
    return openDB(DB_NAME, 1, {
        upgrade(db) {
            db.createObjectStore(STORE_NAME, { keyPath: 'id', autoIncrement: true });
        }
    });
}

export async function saveGridToDB(grid) {
    const db = await getDB();
    const tx = db.transaction(STORE_NAME, 'readwrite');
    tx.store.put(grid);
    return tx.done;
}

export async function getAllUnsyncedGrids() {
    const db = await getDB();
    return db.getAll(STORE_NAME);
}

export async function deleteGridFromDB(id) {
    const db = await getDB();
    const tx = db.transaction(STORE_NAME, 'readwrite');
    tx.store.delete(id);
    return tx.done;
}
