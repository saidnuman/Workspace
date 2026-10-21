// ============================================
// YEŞİL VADI - JAVASCRIPT
// ============================================

// Sayfa yüklendiğinde çalışır
document.addEventListener('DOMContentLoaded', function() {
    console.log('🌿 Yeşil Vadi sitesi yüklendi!');
    
    // === FORM İŞLEMLERİ ===
    const form = document.querySelector('form');
    const mesajListesi = document.getElementById('mesaj-listesi');
    
    if (form) {
        // Sayfa ilk açıldığında kayıtlı mesajları göster
        mesajlariGoster();
        
        form.addEventListener('submit', function(e) {
            e.preventDefault(); // Sayfanın yenilenmesini engeller
            
            // Formdaki değerleri al
            const ad = form.querySelector('input[type="text"]').value;
            const email = form.querySelector('input[type="email"]').value;
            const mesaj = form.querySelector('textarea').value;
            
            // Boş mu kontrol et
            if (!ad || !email || !mesaj) {
                alert('Lütfen tüm alanları doldurun! 🌿');
                return;
            }
            
            // Mesajı kaydet
            const yeniMesaj = {
                ad: ad,
                email: email,
                mesaj: mesaj,
                tarih: new Date().toLocaleString('tr-TR')
            };
            
            // Önceki mesajları al (varsa)
            let mesajlar = JSON.parse(localStorage.getItem('mesajlar')) || [];
            mesajlar.push(yeniMesaj);
            
            // localStorage'a kaydet (telefon hafızasına!)
            localStorage.setItem('mesajlar', JSON.stringify(mesajlar));
            
            // Kullanıcıya bildir
            alert('Teşekkürler ' + ad + '! Mesajınız alındı. 🌿');
            
            // Formu temizle
            form.reset();
            
            // Mesajları yeniden göster
            mesajlariGoster();
        });
    }
    
    // === MESAJLARI GÖSTER FONKSİYONU ===
    function mesajlariGoster() {
        if (!mesajListesi) return;
        
        const mesajlar = JSON.parse(localStorage.getItem('mesajlar')) || [];
        
        if (mesajlar.length === 0) {
            mesajListesi.innerHTML = '<p style="text-align:center; color:#888;">Henüz mesaj yok. İlk mesajı siz gönderin! 🌿</p>';
            return;
        }
        
        let html = '<h3 style="color:#2d5a27; margin-bottom:1rem;">📬 Gelen Mesajlar</h3>';
        
        // Son mesajdan başla (ters çevir)
        mesajlar.slice().reverse().forEach(function(m) {
            html += `
                <div style="background:#fff; padding:1rem; margin-bottom:1rem; border-radius:10px; border-left:4px solid #6b9e23; box-shadow:0 2px 10px rgba(0,0,0,0.05);">
                    <strong style="color:#2d5a27;">${m.ad}</strong> 
                    <span style="color:#888; font-size:0.85rem;">(${m.tarih})</span>
                    <p style="margin-top:0.5rem; color:#555;">${m.mesaj}</p>
                </div>
            `;
        });
        
        mesajListesi.innerHTML = html;
    }
    
    // === GALERİ LIGHTBOX ===
    const galeriResimleri = document.querySelectorAll('img');
    galeriResimleri.forEach(function(img) {
        img.style.cursor = 'pointer';
        img.addEventListener('click', function() {
            // Lightbox div'i oluştur
            const lightbox = document.createElement('div');
            lightbox.style.cssText = `
                position: fixed;
                top: 0; left: 0;
                width: 100%; height: 100%;
                background: rgba(0,0,0,0.9);
                display: flex;
                justify-content: center;
                align-items: center;
                z-index: 1000;
                cursor: zoom-out;
            `;
            
            const buyukResim = document.createElement('img');
            buyukResim.src = img.src;
            buyukResim.style.cssText = `
                max-width: 90%;
                max-height: 90%;
                border-radius: 10px;
                box-shadow: 0 0 30px rgba(255,255,255,0.2);
            `;
            
            lightbox.appendChild(buyukResim);
            document.body.appendChild(lightbox);
            
            // Tıklayınca kapat
            lightbox.addEventListener('click', function() {
                lightbox.remove();
            });
        });
    });
});
