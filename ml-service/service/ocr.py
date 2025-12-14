import pytesseract

def invoke_ocr(path_to_image: str):
  """This function retrieve path to ID card image
  and extract text from it.
  """
  result = pytesseract.image_to_string(path_to_image)
  return result
  