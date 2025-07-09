import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/total_patient_widget.dart';
import '../../../../../../generated/l10n.dart';
import 'home_doctor_info.dart';
import 'no_patient_available.dart';

class DoctorHomeViewBody extends StatelessWidget {
  const DoctorHomeViewBody({super.key});

  @override
  Widget build(BuildContext context) {
    return CustomScrollView(
      slivers: [
        SliverToBoxAdapter(
          child: Column(
            children: [
              HomeDoctorInfo(),
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 16.0),
                child: Column(
                  children: [
                    SizedBox(height: 25.5),
                    TotalPatientWidget(),
                    SizedBox(height: 24),
                    Row(
                      children: [
                        Text(
                          S.of(context).recent_patient,
                          style: AppTextStyles.popins500style20LightBlackColor,
                        ),
                        Spacer(),
                        Text(
                          S.of(context).see_all,
                          style: AppTextStyles.popins400style12kPrimaryColor,
                        ),
                        SizedBox(width: 3),
                      ],
                    ),
                    SizedBox(height: 12),
                    NoPatientAvailable(),
                  ],
                ),
              ),
            ],
          ),
        ),
        // SliverList(
        //   delegate: SliverChildBuilderDelegate(
        //     (context, index) => Padding(
        //       padding: const EdgeInsets.symmetric(
        //         horizontal: 16.0,
        //         vertical: 4,
        //       ),
        //       child: PatientInfoCard(),
        //     ),
        //     childCount: 3,
        //   ),
        // ),
        // SliverToBoxAdapter(child: SizedBox(height: 30)),
      ],
    );
  }
}
