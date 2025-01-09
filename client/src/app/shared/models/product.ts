export interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  pictureUrl: string;
  product_type: string;
  product_brand: string;
}

export class Product implements Product {}
