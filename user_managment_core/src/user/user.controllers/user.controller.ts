import { Controller, Get, Post, Body, Patch, Param, Delete, Inject } from '@nestjs/common';

import { CreateRegistracionDto } from '../dto/create-registracion.dto';
import { UpdateRegistracionDto } from '../dto/update-registracion.dto';
import { UserRepository } from '../user.repository/userRepository.service';
import { ApiOperation } from '@nestjs/swagger';
import { RedisService } from '../user.services/redis.service';
import { UserInterface } from '../user.repository/contracts/user.repository.Interface';



@Controller('registracion')
export class RegistracionController {
  constructor(@Inject("UserInterface")private readonly UserService: UserInterface,
  private readonly redisService:RedisService) {}

  @Post('register')
  @ApiOperation({ summary: 'Register a new user' })
  async register(@Body() body: CreateRegistracionDto) {
    
    await this.redisService.redisregisterUser(body); //redis 
  
    return   await this.UserService.register(body);

  }


  @Get('findAll')
  async findAll() {  
    // const resRedis=await this.redisService.findAll(); //redis
    return await this.UserService.findAll();
  }


  @Get('findOne/:id')
  async findOne(@Param('id') id: string) {

     const resRedis=await this.redisService.redisfindUser(id) //redis 
     if(resRedis){
       return resRedis
     }

    return await this.UserService.getProfileById(id);
  }


  @Patch('update/:id')
  async update(@Param('id') id: string, @Body() updateRegistracionDto: UpdateRegistracionDto) {

    await this.redisService.redisupdateUser(id,updateRegistracionDto);

    return await this.UserService.updateUser(id, updateRegistracionDto);
  }
  

  @Delete('delete/:id')
  async remove(@Param('id') id: string) {
    console.log("Received ID:", id);  //  Debugging
    await this.redisService.redisdelete(id);
    // return await this.registracionService.remove(+id);
  }
  
}
