
export async function get(url) {
    try {
        const response = await fetch(url)
        if(response !== undefined && response.ok){
            return await response.json()
        }
    } catch (error) {
        console.log(error)
    }
    return null;
}