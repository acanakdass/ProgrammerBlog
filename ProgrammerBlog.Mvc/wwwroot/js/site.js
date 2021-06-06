
const convertFirstLetterToUpperCase = (text) => {
    var convertedText = text.charAt(0).toUpperCase() + text.slice(1);
    return convertedText;
}

const convertToShorterDate = (dateString) => {
    const shorterDate = new Date(dateString).toLocaleDateString('tr-TR');
    return shorterDate;
}