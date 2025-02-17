import { useState, useEffect } from "react";
import axios from "axios";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import "../index.css"; 



const AllRecords = () => {
    const [records, setRecords] = useState([]);
    const [plate, setPlate] = useState("");
    const [ownerName, setOwnerName] = useState("");
    const [apartmentNo, setApartmentNo] = useState("");
    const [dateFrom, setDateFrom] = useState(null);
    const [dateTo, setDateTo] = useState(null);
    const [minHours, setMinHours] = useState("");
    const [minMinutes, setMinMinutes] = useState("");
    const [maxHours, setMaxHours] = useState("");
    const [maxMinutes, setMaxMinutes] = useState("");
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false);
    const [userRole, setUserRole] = useState(null);

    useEffect(() => {
        fetchRecords(); // Sayfa yüklendiğinde verileri getir
        setUserRole(localStorage.getItem("role")); // Kullanıcının rolünü al
    }, []);

    //apiden kayıtları çekiyoruz
    const fetchRecords = async () => {  
        setLoading(true);
        setError(null);

        const token = localStorage.getItem("token");  //token kontrol�
        if (!token) {
            setError("Yetkilendirme hatası: Token bulunamadı! Lütfen giriş yap�n.");
            setLoading(false);
            return;
        }

        try {
            const response = await axios.get("https://localhost:7172/api/vehicle/history", {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });

            console.log("API'den gelen kay�tlar:", response.data);
            setRecords(response.data);
        } catch (err) {
            console.error("Kayıtları çekerken hata oluştu:", err);
            setError("Veriler al�n�rken hata olu�tu! Yetkiniz olmayabilir.");
        } finally {
            setLoading(false);
        }
    };  

    const handleFilter = async () => {
        const filterCriteria = {};
        const token = localStorage.getItem("token");

        if (!token) {
            setError("Yetkilendirme hatası: Token bulunamadı! Lütfen giriş yapın.");
            return;
        }

        if (plate.trim()) filterCriteria.plate = plate.trim();
        if (ownerName.trim()) filterCriteria.ownerName = ownerName.trim();
        if (apartmentNo.trim()) filterCriteria.apartmentNo = apartmentNo.trim();
        if (dateFrom) filterCriteria.dateFrom = dateFrom.toISOString();
        if (dateTo) filterCriteria.dateTo = dateTo.toISOString();

        // S�re hesaplama (saat * 60 + dakika)
        const minDuration = (parseInt(minHours || 0) * 60) + parseInt(minMinutes || 0);
        const maxDuration = (parseInt(maxHours || 0) * 60) + parseInt(maxMinutes || 0);

        if (minDuration > 0) filterCriteria.minDuration = minDuration;
        if (maxDuration > 0) filterCriteria.maxDuration = maxDuration;

        const query = new URLSearchParams(filterCriteria).toString();
        console.log(`API Request: https://localhost:7172/api/vehicle/history/filter?${query}`);

        try {
            const response = await axios.get(`https://localhost:7172/api/vehicle/history/filter?${query}`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });

            console.log("Filtrelenmiş API yanıtı:", response.data);
            setRecords(response.data);
        } catch (err) {
            console.error("Filtreleme sırasında hata oluştu:", err);
            setError("Filtreleme sırasında hata oluştu! Yetkiniz olmayabilir.");
        }
    };

    const handleResetFilters = () => {
        setPlate("");
        setOwnerName("");
        setApartmentNo("");
        setDateFrom(null);
        setDateTo(null);
        setMinHours("");
        setMinMinutes("");
        setMaxHours("");
        setMaxMinutes("");
        fetchRecords(); // Tüm kayıtları yeniden getir
    };

    const formatDuration = (entryTime, exitTime) => {
        if (!exitTime) return "-"; 

        const durationInMinutes = Math.round((new Date(exitTime) - new Date(entryTime)) / (1000 * 60));
        const hours = Math.floor(durationInMinutes / 60); // Kaç saat
        const minutes = durationInMinutes % 60; // Kaç dakika

        if (hours > 0) {
            return `${hours} saat ${minutes} dk`;
        } else {
            return `${minutes} min`;
        }
    };

    const handleDelete = async (id) => {
        const token = localStorage.getItem("token");
        if (!token) {
            setError("Yetkilendirme hatası: Token bulunamadı! Lütfen giriş yapın.");
            return;
        }

        if (!window.confirm("Bu kaydı silmek istediğinize emin misiniz?")) return;

        try {
            await axios.delete(`https://localhost:7172/api/vehicle/${id}`, {
                headers: { Authorization: `Bearer ${token}` },
            });

            alert("Kayıt başarıyla silindi!");
            fetchRecords(); // Listeyi g�ncelle
        } catch (err) {
            console.error("Silme işlemi başarısız:", err);
            setError("Kayıt silinirken hata oluştu! Yetkiniz olmayabilir.");
        }
    };

    return (
        <div className="all-records">
            {/* Hata veya Y�kleniyor Mesaj� */}
            {loading && <div className="alert alert-info">Veriler yükleniyor...</div>}
            {error && <div className="alert alert-error">{error}</div>}

            {/* Filtreleme Alan� */}
            <div className="filter-container">
                <input type="text" placeholder="Plaka" value={plate} onChange={(e) => setPlate(e.target.value)} />
                <input type="text" placeholder="Isim" value={ownerName} onChange={(e) => setOwnerName(e.target.value)} />
                <input type="text" placeholder="DaireNo" value={apartmentNo} onChange={(e) => setApartmentNo(e.target.value)} />
                <DatePicker selected={dateFrom} onChange={(date) => setDateFrom(date)} placeholderText="Tarihten itibaren" />
                <DatePicker selected={dateTo} onChange={(date) => setDateTo(date)} placeholderText="Bu tarihe kadar" />

                {/* Minimum Duration */}
                <div>
                    <input type="number" placeholder="Minimum Saat" value={minHours} onChange={(e) => setMinHours(e.target.value)} />
                    <input type="number" placeholder="Minimum Dakika" value={minMinutes} onChange={(e) => setMinMinutes(e.target.value)} />
                </div>

                {/* Maximum Duration */}
                <div>
                    <input type="number" placeholder="Maximum Saat" value={maxHours} onChange={(e) => setMaxHours(e.target.value)} />
                    <input type="number" placeholder="Maximum Dakika" value={maxMinutes} onChange={(e) => setMaxMinutes(e.target.value)} />
                </div>

                <button onClick={handleFilter}>Filtrele</button>
                <button onClick={handleResetFilters} className="reset-btn">Sıfırla</button>
            </div>

            {/* Tablo Alan� */}
            <div className="table-container"> 
            <table>
                <thead>
                    <tr>
                        <th>Plaka</th>
                        <th>İsim</th>
                        <th>DaireNo</th>
                        <th>Giriş Zamanı</th>
                        <th>Çıkış Zamanı</th>
                        <th>Geçirilen Süre</th>
                        {userRole === "SuperAdmin" && <th>Sil</th>}
                    </tr>
                </thead>
                <tbody>
                    {records.map((record) => (
                        <tr key={record.id}>
                            <td>{record.licensePlate}</td>
                            <td>{record.ownerName}</td>
                            <td>{record.apartmentNumber}</td>
                            <td>{new Date(record.entryTime).toLocaleString()}</td>
                            <td>{record.exitTime ? new Date(record.exitTime).toLocaleString() : "Hala Otoparkta"}</td>
                            <td>{formatDuration(record.entryTime, record.exitTime)}</td>
                            {userRole === "SuperAdmin" && (
                                <td>
                                    <button className="delete-btn" onClick={() => handleDelete(record.id)}>Sil</button>
                                </td>
                            )}
                        </tr>
                    ))}
                </tbody>
            </table>
            </div>
        </div>
        
    );
};

export default AllRecords;
