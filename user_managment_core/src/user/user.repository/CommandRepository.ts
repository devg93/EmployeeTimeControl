import { Injectable } from '@nestjs/common';
import * as bcrypt from 'bcrypt';
import { CreateRegistracionDto } from '../dto/create-registracion.dto';
import { UpdateRegistracionDto } from '../dto/update-registracion.dto';
import { InjectRepository } from '@nestjs/typeorm';
import { User } from '../entities/registracion.entity';
import { Repository } from 'typeorm';
import { IuserCommandRepository } from '../contracts/user.repository.Interface';



@Injectable()
export class UserCommandRepository implements IuserCommandRepository {
  constructor(
    @InjectRepository(User) private readonly userRepository: Repository<User>,
  ) { }
  //*********************************************************** */
  async register(body: CreateRegistracionDto) {

  //  const newUser = this.userRepository.create(body);

    const newUser = this.userRepository.create({
      userName: body.userName, 
      email: body.email,
      password:await bcrypt.hash(body.password, 10) ,
      iPadrres: body.iPadrres,
      deviceName: body.deviceName,
  });
  

  
    const userIsSaved = await this.userRepository.save(newUser);
    if (userIsSaved) return "user is created"
  }


  //*********************************************************** */
  async findAll(): Promise<User[]> {
    return await this.userRepository.find();
  }
  //*********************************************************** */
  async findOneUser(id: string) {
    return await this.userRepository.findOne({ where: { id } });

  }
  //*********************************************************** */
  async updateUser(id: string, updateRegistracionDto: UpdateRegistracionDto) {
    const user = await this.findOneUser(id);
    if (user == null) return "user not found"

    return await this.userRepository.update(id, updateRegistracionDto) ?? "user not updated"
  }
  //*********************************************************** */
  async remove(id: string) {
    return await this.userRepository.delete(id);
  }

  async deleteByemail(email: string): Promise<any> {
    return await this.userRepository.delete({ email });
  }


}
