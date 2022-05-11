import React, { useState } from 'react';
import axios from 'axios';

const API = "https://localhost:44301/api/";

export const Album = () => {

    const [name, setName] = useState("");
    const [year, setYear] = useState(0);
    const [cover, setCover] = useState(null);

    const submitHandle = async (event) => {
        event.preventDefault();

        let formData = new FormData();
        formData.append("name", name);
        formData.append("year", year);
        formData.append("cover", cover);

        axios.post(API + "album", formData)
            .then(response => alert("Album was successfully posted!"))
            .catch(error => {
                alert("Error with posting!");
            });
    }

    return (
        <>
        <h2>Create New Album</h2>
        <form onSubmit={submitHandle}>
            <label>Name:
                <input type="text" 
                       onChange={(event) => setName(event.target.value)}/>
            </label>
            <label>Year: 
                <input type="number" 
                       onChange={(event) => setYear(event.target.value)}/>
            </label>
            <label>Cover: 
                <input type="file" 
                       onChange={(event) => setCover(event.target.files[0])}/>
            </label>
            <button type="submit">Create</button>
        </form>
        </>
    );
}