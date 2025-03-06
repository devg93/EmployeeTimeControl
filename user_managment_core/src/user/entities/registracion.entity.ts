import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity()
export class User {
  @PrimaryGeneratedColumn()
  id: string;

  @Column({ type: "varchar", length: 55, nullable: false }) 
  userName: string;

  @Column()
  password: string;

  @Column()
  email: string

  @Column()
  iPadrres: string;

  @Column()
  deviceName: string;

  @Column({ default: false })
  staus: boolean=false;
}
