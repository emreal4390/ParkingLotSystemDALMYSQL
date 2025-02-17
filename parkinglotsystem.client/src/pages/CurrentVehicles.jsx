﻿import { useEffect, useState } from "react";
import axios from "axios";
import "../index.css";

const CurrentVehicles = () => {
    const [vehicles, setVehicles] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const token = localStorage.getItem("token");

        if (!token) {
            setError("Yetkilendirme hatası: Kullanıcı giriş yapmamış!");
            return;
        }

        axios.get("https://localhost:7172/api/vehicle/active", {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
            .then(response => {
                setVehicles(response.data);
                setLoading(false);
            })
            .catch(error => {
                console.error("API Hatası:", error);
                setError("API'ye erişim hatası!");
                setLoading(false);
            });
    }, []);
    return (
        <div>
            {loading && <p>Veriler yükleniyor...</p>}
            {error && <p style={{ color: "red" }}>{error}</p>}
            <table>
                <thead>
                    <tr>
                        <th>Plaka</th>
                        <th>İsim</th>
                        <th>DaireNo</th>
                        <th>Giriş Zamanı</th>
                    </tr>
                </thead>
                <tbody>
                    {vehicles.length > 0 ? (
                        vehicles.map(vehicle => (
                            <tr key={vehicle.id}>
                                <td>{vehicle.licensePlate}</td>
                                <td>{vehicle.ownerName}</td>
                                <td>{vehicle.apartmentNumber}</td>
                                <td>{new Date(vehicle.entryTime).toLocaleString()}</td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan="4">Otoparkta şuan araç yok.</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
};

export default CurrentVehicles;
